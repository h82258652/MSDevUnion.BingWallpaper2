using Newtonsoft.Json;
using System;

namespace BingoWallpaper.Model
{
    [JsonObject]
    public class AVObject : LeanCloudResultBase
    {
        [JsonProperty("objectId")]
        public string ObjectId
        {
            get;
            set;
        }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt
        {
            get;
            set;
        }

        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt
        {
            get;
            set;
        }
    }
}