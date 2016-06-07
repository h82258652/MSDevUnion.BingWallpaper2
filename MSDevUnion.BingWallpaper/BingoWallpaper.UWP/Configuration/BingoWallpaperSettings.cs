﻿using BingoWallpaper.Models;
using Windows.Storage;

namespace BingoWallpaper.Uwp.Configuration
{
    public class BingoWallpaperSettings : AppSettingsBase, IBingoWallpaperSettings
    {
        public static IBingoWallpaperSettings Current
        {
            get;
        } = new BingoWallpaperSettings();

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
    }
}