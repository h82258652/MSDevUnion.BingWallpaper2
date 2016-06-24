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
        private readonly ILeanCloudWallpaperService _leanCloudWallpaperService;

        private readonly IBingoWallpaperSettings _bingoWallpaperSettings;

        private RelayCommand _clearDataCacheCommand;

        public SettingViewModel(ILeanCloudWallpaperService leanCloudWallpaperService, IBingoWallpaperSettings bingoWallpaperSettings)
        {
            _leanCloudWallpaperService = leanCloudWallpaperService;
            _bingoWallpaperSettings = bingoWallpaperSettings;
        }

        public IReadOnlyList<string> Areas => _leanCloudWallpaperService.GetSupportedAreas();

        public bool AutoUpdateLockScreen
        {
            get
            {
                return _bingoWallpaperSettings.AutoUpdateLockScreen;
            }
            set
            {
                _bingoWallpaperSettings.AutoUpdateLockScreen = value;
                RaisePropertyChanged();
            }
        }

        public bool AutoUpdateWallpaper
        {
            get
            {
                return _bingoWallpaperSettings.AutoUpdateWallpaper;
            }
            set
            {
                _bingoWallpaperSettings.AutoUpdateWallpaper = value;
                RaisePropertyChanged();
            }
        }

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

        public IReadOnlyList<SaveLocation> SaveLocations
        {
            get;
        } = Enum.GetValues(typeof(SaveLocation)).Cast<SaveLocation>().ToList();

        public string SelectedArea
        {
            get
            {
                return _bingoWallpaperSettings.SelectedArea;
            }
            set
            {
                _bingoWallpaperSettings.SelectedArea = value;
                RaisePropertyChanged();
            }
        }

        public SaveLocation SelectedSaveLocation
        {
            get
            {
                return _bingoWallpaperSettings.SelectedSaveLocation;
            }
            set
            {
                _bingoWallpaperSettings.SelectedSaveLocation = value;
                RaisePropertyChanged();
            }
        }

        public WallpaperSize SelectedWallpaperSize
        {
            get
            {
                return _bingoWallpaperSettings.SelectedWallpaperSize;
            }
            set
            {
                _bingoWallpaperSettings.SelectedWallpaperSize = value;
                RaisePropertyChanged();
            }
        }

        public IReadOnlyList<WallpaperSize> WallpaperSizes => _leanCloudWallpaperService.GetSupportedWallpaperSizes();
    }
}