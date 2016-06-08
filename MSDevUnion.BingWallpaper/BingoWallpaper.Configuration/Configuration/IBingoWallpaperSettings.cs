using BingoWallpaper.Models;
using System.ComponentModel;

namespace BingoWallpaper.Configuration
{
    public interface IBingoWallpaperSettings : INotifyPropertyChanged
    {
        bool AutoUpdateLockScreen
        {
            get;
            set;
        }

        bool AutoUpdateWallpaper
        {
            get;
            set;
        }

        string SelectedArea
        {
            get;
            set;
        }

        SaveLocation SelectedSaveLocation
        {
            get;
            set;
        }

        WallpaperSize SelectedWallpaperSize
        {
            get;
            set;
        }
    }
}