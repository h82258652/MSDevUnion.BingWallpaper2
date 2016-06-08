using BingoWallpaper.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace BingoWallpaper.Services
{
    public class WallpaperService : IWallpaperService
    {
        public virtual Task<LeanCloudResultCollection<Archive>> GetArchivesAsync(int year, int month, string area)
        {
            var viewMonth = new DateTime(year, month, 1);
            return GetArchivesAsync(viewMonth, area);
        }

        public virtual async Task<Image> GetImageAsync(string objectId)
        {
            if (objectId == null)
            {
                throw new ArgumentNullException(nameof(objectId));
            }
            if (objectId.Length <= 0)
            {
                throw new ArgumentException($"{nameof(objectId)} 不能为空字符串。");
            }

            string requestUrl = $"{Constants.LeanCloudUrlBase}/1.1/classes/Image/{objectId}";

            using (var client = CreateHttpClient())
            {
                var json = await client.GetStringAsync(requestUrl);
                return JsonConvert.DeserializeObject<Image>(json);
            }
        }

        public virtual async Task<LeanCloudResultCollection<Image>> GetImagesAsync(IEnumerable<string> objectIds)
        {
            if (objectIds == null)
            {
                throw new ArgumentNullException(nameof(objectIds));
            }

            var where = new
            {
                objectId = new Dictionary<string, IEnumerable<string>>()
                {
                    {
                        "$in",
                        objectIds
                    }
                }
            };

            string requestUrl = $"{Constants.LeanCloudUrlBase}/1.1/classes/Image?where={WebUtility.UrlEncode(JsonConvert.SerializeObject(where))}&order=-updatedAt";

            using (var client = CreateHttpClient())
            {
                var json = await client.GetStringAsync(requestUrl);
                return JsonConvert.DeserializeObject<LeanCloudResultCollection<Image>>(json);
            }
        }

        public Task<LeanCloudResultCollection<Image>> GetImagesAsync(params string[] objectIds)
        {
            return GetImagesAsync((IEnumerable<string>)objectIds);
        }

        public IReadOnlyList<string> GetSupportedAreas()
        {
            return new[]
            {
                "de-DE",
                "en-AU",
                "en-CA",
                "en-GB",
                "en-IN",
                "en-US",
                "fr-CA",
                "fr-FR",
                "ja-JP",
                "pt-BR",
                "zh-CN",
            };
        }

        public virtual IReadOnlyList<WallpaperSize> GetSupportedWallpaperSizes()
        {
            return new[]
            {
                new WallpaperSize(150,150),
                new WallpaperSize(200,200),
                new WallpaperSize(240,240),
                new WallpaperSize(240,400),
                new WallpaperSize(310,150),
                new WallpaperSize(320,180),
                new WallpaperSize(400,240),
                new WallpaperSize(480,800),
                new WallpaperSize(640,360),
                new WallpaperSize(720,1280),
                new WallpaperSize(768,1024),
                new WallpaperSize(768,1280),
                new WallpaperSize(768,1366),
                new WallpaperSize(800,480),
                new WallpaperSize(800,600),
                new WallpaperSize(1024,768),
                new WallpaperSize(1080,1920),
                new WallpaperSize(1280,720),
                new WallpaperSize(1280,768),
                new WallpaperSize(1366,768),
                new WallpaperSize(1920,1080),
                new WallpaperSize(1920,1200),
            };
        }

        public virtual string GetUrl(Image image, WallpaperSize size)
        {
            if (image == null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            return Constants.BingUrlBase + image.UrlBase + "_" + size.ToString() + ".jpg";
        }

        public async Task<IEnumerable<Wallpaper>> GetWallpapersAsync(int year, int month, string area)
        {
            var archives = await GetArchivesAsync(year, month, area);
            var imageIds = archives.Select(temp => temp.Image.ObjectId);
            var images = await GetImagesAsync(imageIds);
            return from archive in archives
                   let image = images.FirstOrDefault(temp => temp.ObjectId == archive.Image.ObjectId)
                   select new Wallpaper()
                   {
                       Archive = archive,
                       Image = image
                   };
        }

        protected async Task<LeanCloudResultCollection<Archive>> GetArchivesAsync(DateTime viewMonth, string area)
        {
            ValidateGetArchivesAsyncParameters(viewMonth, area);

            var where = new
            {
                market = area,
                date = new Dictionary<string, string>()
                {
                    {
                        "$regex",
                        @"\Q" + viewMonth.ToString("yyyyMM") + @"\E"
                    }
                }
            };

            string requestUrl = $"{Constants.LeanCloudUrlBase}/1.1/classes/Archive?where={WebUtility.UrlEncode(JsonConvert.SerializeObject(where))}&order=-date";

            using (var client = CreateHttpClient())
            {
                var json = await client.GetStringAsync(requestUrl);
                return JsonConvert.DeserializeObject<LeanCloudResultCollection<Archive>>(json);
            }
        }

        protected void ValidateGetArchivesAsyncParameters(DateTime viewMonth, string area)
        {
            if (viewMonth < Constants.MinimumViewMonth)
            {
                throw new ArgumentOutOfRangeException(nameof(viewMonth), "请求的时间不能早于 2015 年 1 月。");
            }
            if (area == null)
            {
                throw new ArgumentNullException(nameof(area));
            }
            if (area.Length <= 0)
            {
                throw new ArgumentException("请填写 area。", nameof(area));
            }
        }

        private static HttpClient CreateHttpClient()
        {
            var client = new HttpClient();
            var headers = client.DefaultRequestHeaders;
            headers.Add("X-AVOSCloud-Application-Id", Constants.LeanCloudAppId);
            headers.Add("X-AVOSCloud-Application-Key", Constants.LeanCloudAppKey);
            return client;
        }

        public async Task<Archive> GetNewestArchiveAsync(string area)
        {
            using (var client = CreateHttpClient())
            {
                var where = new
                {
                    market = area
                };

                string requestUrl = $"{Constants.LeanCloudUrlBase}/1.1/classes/Archive?where={WebUtility.UrlEncode(JsonConvert.SerializeObject(where))}&order=-date&limit=1";

                var json = await client.GetStringAsync(requestUrl);
                return JsonConvert.DeserializeObject<LeanCloudResultCollection<Archive>>(json).FirstOrDefault();
            }
        }

        public async Task<Wallpaper> GetNewestWallpaperAsync(string area)
        {
            var archive = await GetNewestArchiveAsync(area);
            if (archive == null)
            {
                return null;
            }
            var image = await GetImageAsync(archive.Image.ObjectId);
            return new Wallpaper()
            {
                Archive = archive,
                Image = image
            };
        }
    }
}