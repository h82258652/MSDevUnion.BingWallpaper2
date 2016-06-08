using BingoWallpaper.Models;
using Windows.Storage;

namespace BingoWallpaper.Configuration
{
    public class BingoWallpaperSettings : AppSettingsBase, IBingoWallpaperSettings
    {
        public static IBingoWallpaperSettings Current
        {
            get;
        } = new BingoWallpaperSettings();

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
                // TODO
                return Get(nameof(SelectedArea), () => "", ApplicationDataLocality.Roaming);
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

                // TODO
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
            }
        }
    }
}