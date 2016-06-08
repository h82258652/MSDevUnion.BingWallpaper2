using BingoWallpaper.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BingoWallpaper.Services
{
    public interface IWallpaperService
    {
        Task<LeanCloudResultCollection<Archive>> GetArchivesAsync(int year, int month, string area);

        Task<Image> GetImageAsync(string objectId);

        Task<LeanCloudResultCollection<Image>> GetImagesAsync(IEnumerable<string> objectIds);

        Task<LeanCloudResultCollection<Image>> GetImagesAsync(params string[] objectIds);

        IReadOnlyList<string> GetSupportedAreas();

        IReadOnlyList<WallpaperSize> GetSupportedWallpaperSizes();

        string GetUrl(Image image, WallpaperSize size);

        Task<IEnumerable<Wallpaper>> GetWallpapersAsync(int year, int month, string area);

        Task<Archive> GetNewestArchiveAsync(string area);

        Task<Wallpaper> GetNewestWallpaperAsync(string area);
    }
}