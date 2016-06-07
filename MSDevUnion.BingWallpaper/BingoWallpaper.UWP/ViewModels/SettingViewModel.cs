using BingoWallpaper.Models;
using BingoWallpaper.Uwp.Configuration;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BingoWallpaper.Uwp.ViewModels
{
    public class SettingViewModel : ViewModelBase
    {
        private readonly IBingoWallpaperSettings _settings;

        public SettingViewModel(IBingoWallpaperSettings settings)
        {
            _settings = settings;
        }

        public IReadOnlyList<string> Areas
        {
            get
            {
                // TODO
                throw new NotImplementedException();
            }
        }

        public IReadOnlyList<SaveLocation> SaveLocations
        {
            get;
        } = Enum.GetValues(typeof(SaveLocation)).Cast<SaveLocation>().ToList();

        public string SelectedArea
        {
            get
            {
                return _settings.SelectedArea;
            }
            set
            {
                _settings.SelectedArea = value;
                RaisePropertyChanged();
            }
        }

        public SaveLocation SelectedSaveLocation
        {
            get
            {
                return _settings.SelectedSaveLocation;
            }
            set
            {
                _settings.SelectedSaveLocation = value;
                RaisePropertyChanged();
            }
        }

        public WallpaperSize SelectedWallpaperSize
        {
            get
            {
                return _settings.SelectedWallpaperSize;
            }
            set
            {
                _settings.SelectedWallpaperSize = value;
                RaisePropertyChanged();
            }
        }

        public IReadOnlyList<WallpaperSize> WallpaperSizes
        {
            get
            {
                return WallpaperSize.SupportSizes;
            }
        }
    }
}