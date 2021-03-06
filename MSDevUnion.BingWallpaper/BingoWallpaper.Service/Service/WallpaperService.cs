﻿using BingoWallpaper.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace BingoWallpaper.Service
{
    public class WallpaperService : IWallpaperService
    {
        public async Task<LeanCloudResultCollection<Archive>> GetArchivesAsync(int year, int month, string area)
        {
            var viewMonth = new DateTime(year, month, 1);
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

        public async Task<Image> GetImageAsync(string objectId)
        {
            if (objectId == null)
            {
                throw new ArgumentNullException(nameof(objectId));
            }
            if (objectId.Length <= 0)
            {
                throw new ArgumentException("objectId 不能为空字符串。");
            }

            string requestUrl = $"{Constants.LeanCloudUrlBase}/1.1/classes/Image/{objectId}";

            using (var client = CreateHttpClient())
            {
                var json = await client.GetStringAsync(requestUrl);
                return JsonConvert.DeserializeObject<Image>(json);
            }
        }

        public async Task<LeanCloudResultCollection<Image>> GetImagesAsync(IEnumerable<string> objectIds)
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

        private static HttpClient CreateHttpClient()
        {
            var client = new HttpClient();
            var headers = client.DefaultRequestHeaders;
            headers.Add("X-AVOSCloud-Application-Id", Constants.LeanCloudAppId);
            headers.Add("X-AVOSCloud-Application-Key", Constants.LeanCloudAppKey);
            return client;
        }
    }
}