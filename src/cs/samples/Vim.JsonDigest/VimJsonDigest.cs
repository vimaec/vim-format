﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Vim.JsonDigest
{
    /// <summary>
    /// This class represents aggregated data from a VIM file about its rooms, areas, and materials.
    /// This class is intended to be serialized to JSON for subsequent analysis in a data pipeline.
    /// </summary>
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
            : this(VimScene.LoadVim(stream))
        { }

        public JObject ToJObject()
            => JObject.FromObject(this);

        public string ToJson(Formatting formatting = Formatting.Indented)
            => ToJObject().ToString(formatting);

        public static VimJsonDigest FromJson(string jsonContent)
            => JsonConvert.DeserializeObject<VimJsonDigest>(jsonContent);
    }
}
