﻿using BingoWallpaper.Models;
using System;
using Windows.Storage;

namespace BingoWallpaper.Uwp.Configuration
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
                // TODO
                var temp = Get(nameof(SelectedWallpaperSize), () => "", ApplicationDataLocality.Local);
                throw new NotImplementedException();
            }
            set
            {
                // TODO
                Set(nameof(SelectedWallpaperSize), value);
            }
        }
    }
}