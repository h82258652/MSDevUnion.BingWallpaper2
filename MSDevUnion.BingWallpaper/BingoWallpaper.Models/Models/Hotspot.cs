using Newtonsoft.Json;
using System;

namespace BingoWallpaper.Models
{
    [JsonObject]
    public class Hotspot
    {
        [JsonIgnore]
        public Guid Id
        {
            get;
            set;
        }

        [JsonProperty("desc")]
        public string Description
        {
            get;
            set;
        }

        [JsonProperty("link")]
        public string Link
        {
            get;
            set;
        }

        [JsonProperty("query")]
        public string Query
        {
            get;
            set;
        }

        [JsonProperty("locx")]
        public int LocationX
        {
            get;
            set;
        }

        [JsonProperty("locy")]
        public int LocationY
        {
            get;
            set;
        }
    }
}