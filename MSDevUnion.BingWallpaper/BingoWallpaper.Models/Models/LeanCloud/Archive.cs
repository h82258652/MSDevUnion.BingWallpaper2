using Newtonsoft.Json;
using System.Collections.Generic;

namespace BingoWallpaper.Models.LeanCloud
{
    [JsonObject]
    public class Archive : AVObject
    {
        [JsonProperty("date")]
        public string Date
        {
            get;
            set;
        }

        [JsonProperty("hs")]
        public IReadOnlyList<Hotspot> Hotspots
        {
            get;
            set;
        }

        [JsonProperty("cs")]
        public CoverStory CoverStory
        {
            get;
            set;
        }

        [JsonProperty("msg")]
        public IReadOnlyList<Message> Messages
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

        [JsonProperty("info")]
        public string Info
        {
            get;
            set;
        }

        [JsonProperty("image")]
        public LeanCloudPointer Image
        {
            get;
            set;
        }

        [JsonProperty("market")]
        public string Market
        {
            get;
            set;
        }
    }
}