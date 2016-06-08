using BingoWallpaper.Models;
using BingoWallpaper.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BingoWallpaper.Uwp.Design
{
    public class WallpaperService : IWallpaperService
    {
        public Task<LeanCloudResultCollection<Archive>> GetArchivesAsync(int year, int month, string area)
        {
            throw new NotImplementedException();
        }

        public Task<Image> GetImageAsync(string objectId)
        {
            throw new NotImplementedException();
        }

        public Task<LeanCloudResultCollection<Image>> GetImagesAsync(params string[] objectIds)
        {
            throw new NotImplementedException();
        }

        public Task<LeanCloudResultCollection<Image>> GetImagesAsync(IEnumerable<string> objectIds)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<string> GetSupportedAreas()
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<WallpaperSize> GetSupportedWallpaperSizes()
        {
            throw new NotImplementedException();
        }

        public string GetUrl(Image image, WallpaperSize size)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Wallpaper>> GetWallpapersAsync(int year, int month, string area)
        {
            throw new NotImplementedException();
        }
    }
}