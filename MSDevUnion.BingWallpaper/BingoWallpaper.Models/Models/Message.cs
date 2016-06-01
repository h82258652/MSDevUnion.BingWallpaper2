using Newtonsoft.Json;
using System;

namespace BingoWallpaper.Models
{
    [JsonObject]
    public class Message
    {
        [JsonIgnore]
        public Guid Id
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

        [JsonProperty("text")]
        public string Text
        {
            get;
            set;
        }

        [JsonProperty("title")]
        public string Title
        {
            get;
            set;
        }
    }
}