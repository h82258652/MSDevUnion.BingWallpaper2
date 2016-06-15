using BingoWallpaper.Configuration;
using BingoWallpaper.Models;
using BingoWallpaper.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BingoWallpaper.Uwp.ViewModels
{
    public class SettingViewModel : ViewModelBase
    {
        private readonly IBingoWallpaperSettings _settings;

        private readonly ILeanCloudWallpaperService _leanCloudWallpaperService;

        public SettingViewModel(ILeanCloudWallpaperService leanCloudWallpaperService, IBingoWallpaperSettings settings)
        {
            _leanCloudWallpaperService = leanCloudWallpaperService;
            _settings = settings;
        }

        public IReadOnlyList<string> Areas => _leanCloudWallpaperService.GetSupportedAreas();

        public bool AutoUpdateLockScreen
        {
            get
            {
                return _settings.AutoUpdateLockScreen;
            }
            set
            {
                _settings.AutoUpdateLockScreen = value;
                RaisePropertyChanged();
            }
        }

        public bool AutoUpdateWallpaper
        {
            get
            {
                return _settings.AutoUpdateWallpaper;
            }
            set
            {
                _settings.AutoUpdateWallpaper = value;
                RaisePropertyChanged();
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

        public IReadOnlyList<WallpaperSize> WallpaperSizes => _leanCloudWallpaperService.GetSupportedWallpaperSizes();

        private RelayCommand _clearDataCacheCommand;

        public RelayCommand ClearDataCacheCommand
        {
            get
            {
                _clearDataCacheCommand = _clearDataCacheCommand ?? new RelayCommand(() =>
                {
                    // TODO
                });
                return _clearDataCacheCommand;
            }
        }
    }
}