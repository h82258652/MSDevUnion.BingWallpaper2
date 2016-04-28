using BingoWallpaper.Models;
using BingoWallpaper.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingoWallpaper.UWP.Design
{
    public class WallpaperService : IWallpaperService
    {
        public Task<LeanCloudResultCollection<Archive>> GetArchivesAsync(int year, int month, string market)
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