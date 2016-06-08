using BingoWallpaper.Configuration;
using BingoWallpaper.Models;
using BingoWallpaper.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BingoWallpaper.Uwp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        private readonly IBingoWallpaperSettings _settings;

        private readonly IWallpaperService _wallpaperService;

        private RelayCommand<Wallpaper> _clickCommand;

        private bool _isBusy;

        private RelayCommand _refreshCommand;

        private WallpaperCollection _selectedWallpaperCollection;

        public MainViewModel(INavigationService navigationService, IWallpaperService wallpaperService, IBingoWallpaperSettings settings)
        {
            _navigationService = navigationService;
            _wallpaperService = wallpaperService;
            _settings = settings;

            var wallpaperCollections = new List<WallpaperCollection>();
            var date = Constants.MinimumViewMonth;
            while (date < DateTimeOffset.Now)
            {
                wallpaperCollections.Add(new WallpaperCollection(date.Year, date.Month));
                date = date.AddMonths(1);
            }
            WallpaperCollections = wallpaperCollections;
        }

        public RelayCommand<Wallpaper> ClickCommand
        {
            get
            {
                _clickCommand = _clickCommand ?? new RelayCommand<Wallpaper>(wallpaper =>
                {
                    _navigationService.NavigateTo(ViewModelLocator.DetailViewKey, wallpaper);
                });
                return _clickCommand;
            }
        }

        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                Set(ref _isBusy, value);
            }
        }

        public RelayCommand RefreshCommand
        {
            get
            {
                _refreshCommand = _refreshCommand ?? new RelayCommand(RefreshExecute);
                return _refreshCommand;
            }
        }

        public WallpaperCollection SelectedWallpaperCollection
        {
            get
            {
                if (_selectedWallpaperCollection == null)
                {
                    _selectedWallpaperCollection = WallpaperCollections.LastOrDefault();
                }
                return _selectedWallpaperCollection;
            }
            set
            {
                Set(ref _selectedWallpaperCollection, value);
            }
        }

        public IReadOnlyList<WallpaperCollection> WallpaperCollections
        {
            get;
        }

        private async void RefreshExecute()
        {
            IsBusy = true;
            try
            {
                var selectedWallpaperCollection = SelectedWallpaperCollection;
                selectedWallpaperCollection.Clear();

                var wallpapers = await _wallpaperService.GetWallpapersAsync(selectedWallpaperCollection.Year, selectedWallpaperCollection.Month, _settings.SelectedArea);
                foreach (var wallpaper in wallpapers)
                {
                    selectedWallpaperCollection.Add(wallpaper);
                }
            }
            catch
            {
                // TODO
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}