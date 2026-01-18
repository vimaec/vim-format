using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Vim.Format;

namespace Vim.JsonDigest
{
    /// <summary>
    /// This class represents aggregated data from a VIM file about its rooms, areas, materials, and element parameters.
    /// This class is intended to be serialized to JSON for subsequent analysis in a data pipeline.
    /// </summary>
    public class VimJsonDigest
    {
        [JsonProperty("bimdocuments")]
        public List<BimDocumentDigest> BimDocumentDigestCollection { get; set; }

        [JsonProperty("elements")]
        public List<ElementDigest> ElementDigestCollection { get; set; }

        [JsonProperty("levels")]
        public List<LevelDigest> LevelDigestCollection { get; set; }

        [JsonProperty("rooms")]
        public List<RoomDigest> RoomDigestCollection { get; set; }

        [JsonProperty("areas")]
        public List<AreaDigest> AreaDigestCollection { get; set; }

        [JsonProperty("materials")]
        public List<MaterialDigest> MaterialDigestCollection { get; set; }

        /// <summary>
        /// JSON Constructor used for deserialization.
        /// </summary>
        [JsonConstructor]
        public VimJsonDigest() { }

        /// <summary>
        /// VIM scene constructor used for serialization.
        /// </summary>
        public VimJsonDigest(VIM vim)
        {
            BimDocumentDigestCollection = BimDocumentDigest.GetBimDocumentDigestCollection(vim).ToList(); 
            ElementDigestCollection = ElementDigest.GetElementDigestCollection(vim).ToList();
            LevelDigestCollection = LevelDigest.GetLevelDigestCollection(vim).ToList();
            RoomDigestCollection = RoomDigest.GetRoomDigestCollection(vim).ToList();
            AreaDigestCollection = AreaDigest.GetAreaDigestCollection(vim).ToList();
            MaterialDigestCollection = MaterialDigest.GetMaterialDigestCollection(vim).ToList();
        }

        /// <summary>
        /// Stream constructor used for serialization. Note: this stream must be seekable.
        /// </summary>
        public VimJsonDigest(Stream stream, string vimFilePath)
            : this(VIM.Open(stream, vimFilePath))
        { }

        public JObject ToJObject()
            => JObject.FromObject(this);

        public string ToJson(Formatting formatting = Formatting.Indented)
            => ToJObject().ToString(formatting);

        public static VimJsonDigest FromJson(string jsonContent)
            => JsonConvert.DeserializeObject<VimJsonDigest>(jsonContent);
    }
}
