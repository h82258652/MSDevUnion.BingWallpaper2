using BingoWallpaper.Model;
using BingoWallpaper.Service;
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
    }
}