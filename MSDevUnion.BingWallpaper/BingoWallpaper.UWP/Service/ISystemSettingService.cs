using System.Threading.Tasks;

namespace BingoWallpaper.Uwp.Service
{
    public interface ISystemSettingService
    {
        Task OpenLockScreenSettingAsync();

        Task OpenWallpaperSettingAsync();
    }
}