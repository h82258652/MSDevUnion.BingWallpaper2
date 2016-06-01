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
        private readonly ISettings _settings;

        public SettingViewModel(ISettings settings)
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
                // TODO
                return _settings.Get(nameof(SelectedArea), () => "", ApplicationDataLocality.Roaming);
            }
            set
            {
                _settings.Set(nameof(SelectedArea), value, ApplicationDataLocality.Roaming);
                RaisePropertyChanged();
            }
        }

        public SaveLocation SelectedSaveLocation
        {
            get
            {
                return _settings.Get(nameof(SelectedSaveLocation), () => SaveLocation.PictureLibrary);
            }
            set
            {
                _settings.Set(nameof(SelectedSaveLocation), value);
                RaisePropertyChanged();
            }
        }

        public string SelectedWallpaperSize
        {
            get
            {
                // TODO
                return _settings.Get(nameof(SelectedWallpaperSize), () => "");
            }
            set
            {
                _settings.Set(nameof(SelectedWallpaperSize), value);
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