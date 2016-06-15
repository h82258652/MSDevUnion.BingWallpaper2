using Newtonsoft.Json;

namespace BingoWallpaper.Models.LeanCloud
{
    [JsonObject]
    public class Image : AVObject, IImage
    {
        [JsonProperty("urlbase")]
        public string UrlBase
        {
            get;
            set;
        }

        /// <summary>
        /// 拥有 1920 x 1080 分辨率。
        /// </summary>
        [JsonProperty("wp")]
        public bool ExistWUXGA
        {
            get;
            set;
        }

        [JsonProperty("copyright")]
        public string Copyright
        {
            get;
            set;
        }

        [JsonProperty("name")]
        public string Name
        {
            get;
            set;
        }
    }
}