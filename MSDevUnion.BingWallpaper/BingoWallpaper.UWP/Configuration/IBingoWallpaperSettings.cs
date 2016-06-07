using BingoWallpaper.Models;
using System.ComponentModel;

namespace BingoWallpaper.Uwp.Configuration
{
    public interface IBingoWallpaperSettings : INotifyPropertyChanged
    {
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