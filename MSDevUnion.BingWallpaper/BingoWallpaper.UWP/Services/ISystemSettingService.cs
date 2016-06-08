using System.Threading.Tasks;

namespace BingoWallpaper.Uwp.Services
{
    public interface ISystemSettingService
    {
        Task OpenLockScreenSettingAsync();

        Task OpenWallpaperSettingAsync();

        // TODO 可能需要更改方法参数。
        Task<bool> SetLockScreenAsync(byte[] imageBytes);

        // TODO 可能需要更改方法参数。
        Task<bool> SetWallpaperAsync(byte[] imageBytes);
    }
}