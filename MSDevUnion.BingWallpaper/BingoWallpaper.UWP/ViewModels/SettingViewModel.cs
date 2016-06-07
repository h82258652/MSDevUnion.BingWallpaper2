using BingoWallpaper.Models;
using BingoWallpaper.Uwp.Configuration;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Storage;

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
                SelectedArea = value;
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

        public string SelectedWallpaperSize
        {
            get
            {
                // TODO
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
                RaisePropertyChanged();
            }
        }

        public IReadOnlyList<string> WallpaperSizes
        {
            get
            {
                // TODO
                throw new NotImplementedException();
            }
        }
    }
}