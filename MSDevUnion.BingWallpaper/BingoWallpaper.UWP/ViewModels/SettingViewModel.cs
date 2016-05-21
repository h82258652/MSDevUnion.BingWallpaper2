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

        public string Area
        {
            get
            {
                return _settings.Get(nameof(Area), () => "", ApplicationDataLocality.Roaming);
            }
            set
            {
                _settings.Set(nameof(Area), value, ApplicationDataLocality.Roaming);
                RaisePropertyChanged();
            }
        }

        public IReadOnlyList<string> Areas
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public SaveLocation SaveLocation
        {
            get
            {
                return _settings.Get(nameof(SaveLocation), () => SaveLocation.PictureLibrary);
            }
            set
            {
                _settings.Set(nameof(SaveLocation), value);
                RaisePropertyChanged();
            }
        }

        public IReadOnlyList<SaveLocation> SaveLocations
        {
            get;
        } = Enum.GetValues(typeof(SaveLocation)).Cast<SaveLocation>().ToList();

        public string WallpaperSize
        {
            get
            {
                return _settings.Get(nameof(WallpaperSize), () => "");
            }
            set
            {
                _settings.Set(nameof(WallpaperSize), value);
                RaisePropertyChanged();
            }
        }

        public IReadOnlyList<string> WallpaperSizes
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}