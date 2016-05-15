using BingoWallpaper.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BingoWallpaper.Service
{
    public interface IWallpaperService
    {
        Task<LeanCloudResultCollection<Archive>> GetArchivesAsync(int year, int month, string area);

        Task<Image> GetImageAsync(string objectId);

        Task<LeanCloudResultCollection<Image>> GetImagesAsync(IEnumerable<string> objectIds);

        Task<LeanCloudResultCollection<Image>> GetImagesAsync(params string[] objectIds);
    }
}