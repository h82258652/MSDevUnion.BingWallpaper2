using BingoWallpaper.Configuration;
using BingoWallpaper.Models;
using BingoWallpaper.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Controls;

namespace BingoWallpaper.Uwp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        private readonly IBingoWallpaperSettings _settings;

        private readonly ILeanCloudWallpaperService _leanCloudWallpaperService;

        private RelayCommand<ItemClickEventArgs> _clickCommand;

        private bool _isBusy;

        private RelayCommand _refreshCommand;

        private WallpaperCollection _selectedWallpaperCollection;

        public MainViewModel(INavigationService navigationService, ILeanCloudWallpaperService leanCloudWallpaperService, IBingoWallpaperSettings settings)
        {
            _navigationService = navigationService;
            _leanCloudWallpaperService = leanCloudWallpaperService;
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

        public RelayCommand<ItemClickEventArgs> ClickCommand
        {
            get
            {
                _clickCommand = _clickCommand ?? new RelayCommand<ItemClickEventArgs>(e =>
                {
                    var wallpaper = (Wallpaper)e.ClickedItem;
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
                    SelectedWallpaperCollection = WallpaperCollections.LastOrDefault();
                }
                return _selectedWallpaperCollection;
            }
            set
            {
                Set(ref _selectedWallpaperCollection, value);
                RefreshExecute();
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

                var wallpapers = await _leanCloudWallpaperService.GetWallpapersAsync(selectedWallpaperCollection.Year, selectedWallpaperCollection.Month, _settings.SelectedArea);
                foreach (var wallpaper in wallpapers)
                {
                    selectedWallpaperCollection.Add(wallpaper);
                }
            }
            catch (Exception ex)
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