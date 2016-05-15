using Newtonsoft.Json;

namespace BingoWallpaper.Model
{
    [JsonObject]
    public class Message
    {
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