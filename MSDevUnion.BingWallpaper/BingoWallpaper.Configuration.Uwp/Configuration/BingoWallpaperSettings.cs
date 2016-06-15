using BingoWallpaper.Models;
using BingoWallpaper.Services;
using System;
using System.Globalization;
using System.Linq;
using Windows.Devices.Input;
using Windows.Graphics.Display;
using Windows.Storage;

namespace BingoWallpaper.Configuration
{
    public class BingoWallpaperSettings : AppSettingsBase, IBingoWallpaperSettings
    {
        private readonly ILeanCloudWallpaperService _leanCloudWallpaperService;

        public BingoWallpaperSettings(ILeanCloudWallpaperService leanCloudWallpaperService)
        {
            _leanCloudWallpaperService = leanCloudWallpaperService;
        }

        public bool AutoUpdateLockScreen
        {
            get
            {
                return Get(nameof(AutoUpdateLockScreen), () => false);
            }
            set
            {
                Set(nameof(AutoUpdateLockScreen), value);
                RaisePropertyChanged();
            }
        }

        public bool AutoUpdateWallpaper
        {
            get
            {
                return Get(nameof(AutoUpdateWallpaper), () => false);
            }
            set
            {
                Set(nameof(AutoUpdateWallpaper), value);
                RaisePropertyChanged();
            }
        }

        public string SelectedArea
        {
            get
            {
                return Get(nameof(SelectedArea), () =>
                {
                    var currentCulture = CultureInfo.CurrentCulture.Name;
                    if (_leanCloudWallpaperService.GetSupportedAreas().Contains(currentCulture, StringComparer.OrdinalIgnoreCase))
                    {
                        return currentCulture;
                    }
                    return "en-US";
                }, ApplicationDataLocality.Roaming);
            }
            set
            {
                Set(nameof(SelectedArea), value, ApplicationDataLocality.Roaming);
                RaisePropertyChanged();
            }
        }

        public SaveLocation SelectedSaveLocation
        {
            get
            {
                return Get(nameof(SelectedSaveLocation), () => SaveLocation.PictureLibrary);
            }
            set
            {
                Set(nameof(SelectedSaveLocation), value);
                RaisePropertyChanged();
            }
        }

        public WallpaperSize SelectedWallpaperSize
        {
            get
            {
                var composite = Get(nameof(SelectedWallpaperSize), () => new ApplicationDataCompositeValue());
                object width;
                object height;
                if (composite.TryGetValue(nameof(WallpaperSize.Width), out width) && composite.TryGetValue(nameof(WallpaperSize.Height), out height))
                {
                    if (width is int && height is int)
                    {
                        return new WallpaperSize((int)width, (int)height);
                    }
                }

                #region 获取屏幕宽度

                var rect = PointerDevice.GetPointerDevices().Last().ScreenRect;
                var scale = DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;
                var screenWidth = (int)(rect.Width * scale);

                #endregion 获取屏幕宽度

                var sizes = _leanCloudWallpaperService.GetSupportedWallpaperSizes().Where(temp => temp.Width == screenWidth).ToList();// 寻找适合的尺寸。
                if (sizes.Any())
                {
                    return sizes.First();
                }

                // 无法匹配，默认返回 800x480。
                return new WallpaperSize(800, 480);
            }
            set
            {
                var composite = new ApplicationDataCompositeValue
                {
                    [nameof(WallpaperSize.Width)] = value.Width,
                    [nameof(WallpaperSize.Height)] = value.Height
                };
                Set(nameof(SelectedWallpaperSize), composite);
                RaisePropertyChanged();
            }
        }
    }
}