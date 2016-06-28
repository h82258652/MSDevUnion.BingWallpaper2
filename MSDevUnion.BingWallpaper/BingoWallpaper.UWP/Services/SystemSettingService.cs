using System;
using System.Threading.Tasks;
using Windows.Storage;
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
                // TODO file name
                var file = await ApplicationData.Current.LocalFolder.CreateFileAsync("lockscreen.jpg", CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteBytesAsync(file, imageBytes);
                return await UserProfilePersonalizationSettings.Current.TrySetLockScreenImageAsync(file);
            }
            return false;
        }

        public async Task<bool> SetWallpaperAsync(byte[] imageBytes)
        {
            if (UserProfilePersonalizationSettings.IsSupported())
            {
                // TODO file name
                var file = await ApplicationData.Current.LocalFolder.CreateFileAsync("wallpaper.jpg", CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteBytesAsync(file, imageBytes);
                return await UserProfilePersonalizationSettings.Current.TrySetWallpaperImageAsync(file);
            }
            return false;
        }
    }
}