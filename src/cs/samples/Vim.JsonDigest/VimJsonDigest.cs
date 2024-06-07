using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Vim.Format;

namespace Vim.JsonDigest
{
    public class VimJsonDigest
    {
        [JsonProperty("rooms")]
        public List<RoomInfo> RoomInfoCollection { get; set; }

        [JsonProperty("areas")]
        public List<AreaInfo> AreaInfoCollection { get; set; }

        [JsonProperty("materials")]
        public List<MaterialInfo> MaterialInfoCollection { get; set; }

        /// <summary>
        /// JSON Constructor used for deserialization.
        /// </summary>
        [JsonConstructor]
        public VimJsonDigest() { }

        /// <summary>
        /// VIM scene constructor used for serialization.
        /// </summary>
        public VimJsonDigest(VimScene vimScene)
        {
            RoomInfoCollection = RoomInfo.GetRoomInfoCollection(vimScene).ToList();
            AreaInfoCollection = AreaInfo.GetAreaInfoCollection(vimScene).ToList();
            MaterialInfoCollection = MaterialInfo.GetMaterialInfoCollection(vimScene).ToList();
        }

        /// <summary>
        /// Stream constructor used for serialization. Note: this stream must be seekable.
        /// </summary>
        public VimJsonDigest(Stream stream)
            : this(VimScene.LoadVim(stream, new LoadOptions() { SkipGeometry = true, SkipAssets = true }))
        { }

        public JObject ToJObject()
            => JObject.FromObject(this);

        public string ToJson(Formatting formatting = Formatting.Indented)
            => ToJObject().ToString(formatting);
    }
}
