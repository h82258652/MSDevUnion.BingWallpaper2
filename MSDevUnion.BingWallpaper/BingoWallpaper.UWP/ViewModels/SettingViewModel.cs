﻿using BingoWallpaper.Models;
using BingoWallpaper.Services;
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

        private readonly IWallpaperService _wallpaperService;

        public SettingViewModel(IWallpaperService wallpaperService, IBingoWallpaperSettings settings)
        {
            _wallpaperService = wallpaperService;
            _settings = settings;
        }

        public IReadOnlyList<string> Areas => _wallpaperService.GetSupportedAreas();

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

        public IReadOnlyList<WallpaperSize> WallpaperSizes => _wallpaperService.GetSupportedWallpaperSizes();
    }
}