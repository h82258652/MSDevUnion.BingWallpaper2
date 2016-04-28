using BingoWallpaper.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BingoWallpaper.Services
{
    public interface IWallpaperService
    {
        Task<LeanCloudResultCollection<Archive>> GetArchivesAsync(int year, int month, string market);

        Task<LeanCloudResultCollection<Image>> GetImagesAsync(IEnumerable<string> objectIds);

        Task<LeanCloudResultCollection<Image>> GetImagesAsync(params string[] objectIds);

        Task<Image> GetImageAsync(string objectId);
    }
}