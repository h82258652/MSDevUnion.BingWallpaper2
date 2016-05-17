using System.Threading.Tasks;

namespace BingoWallpaper.Uwp.Services
{
    public interface ISystemSettingService
    {
        Task OpenLockScreenSettingAsync();

        Task OpenWallpaperSettingAsync();
    }
}