using BingoWallpaper.Models;
using BingoWallpaper.Models.LeanCloud;
using BingoWallpaper.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BingoWallpaper.Uwp.Design
{
    public class LeanCloudWallpaperService : ILeanCloudWallpaperService
    {
        public Task<LeanCloudResultCollection<Archive>> GetArchivesAsync(int year, int month, string area)
        {
            throw new NotImplementedException();
        }

        public Task<Image> GetImageAsync(string objectId)
        {
            throw new NotImplementedException();
        }

        public Task<LeanCloudResultCollection<Image>> GetImagesAsync(IEnumerable<string> objectIds)
        {
            throw new NotImplementedException();
        }

        public Task<LeanCloudResultCollection<Image>> GetImagesAsync(params string[] objectIds)
        {
            return GetImagesAsync((IEnumerable<string>)objectIds);
        }

        public Task<Archive> GetNewestArchiveAsync(string area)
        {
            throw new NotImplementedException();
        }

        public Task<Wallpaper> GetNewestWallpaperAsync(string area)
        {
            throw new NotImplementedException();
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

        public IReadOnlyList<WallpaperSize> GetSupportedWallpaperSizes()
        {
            return new[]
            {
                new WallpaperSize(480,800),
                new WallpaperSize(768,1280),
                new WallpaperSize(800,480),
                new WallpaperSize(1080,1920),
                new WallpaperSize(1366,768),
                new WallpaperSize(1920,1080),
                new WallpaperSize(1920,1200),
            };
        }

        public string GetUrl(IImage image, WallpaperSize size)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Wallpaper>> GetWallpapersAsync(int year, int month, string area)
        {
            throw new NotImplementedException();
        }
    }
}