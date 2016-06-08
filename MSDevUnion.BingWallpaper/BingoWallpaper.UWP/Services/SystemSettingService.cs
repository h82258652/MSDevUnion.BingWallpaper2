using System;
using System.Threading.Tasks;
using Windows.System;
using Windows.System.UserProfile;

namespace BingoWallpaper.Uwp.Services
{
    public class SystemSettingService : ISystemSettingService
    {
        public async Task OpenLockScreenSettingAsync()
        {
            await Launcher.LaunchUriAsync(new Uri("ms-settings:lockscreen"));
        }

        public async Task OpenWallpaperSettingAsync()
        {
            await Launcher.LaunchUriAsync(new Uri("ms-settings:personalization"));
        }

        public async Task<bool> SetLockScreenAsync(byte[] imageBytes)
        {
            if (UserProfilePersonalizationSettings.IsSupported())
            {
                return await UserProfilePersonalizationSettings.Current.TrySetLockScreenImageAsync(null);
            }
            return false;
        }

        public async Task<bool> SetWallpaperAsync(byte[] imageBytes)
        {
            if (UserProfilePersonalizationSettings.IsSupported())
            {
                return await UserProfilePersonalizationSettings.Current.TrySetWallpaperImageAsync(null);
            }
            return false;
        }
    }
}