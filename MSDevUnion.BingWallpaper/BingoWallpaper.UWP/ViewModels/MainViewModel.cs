using BingoWallpaper.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BingoWallpaper.Uwp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public IList<ObservableCollection<Models.Archive>> Temp;

        private readonly INavigationService _navigationService;

        private bool _isBusy;

        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            //Initialize();
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

        private RelayCommand _clickCommand;

        public RelayCommand ClickCommand
        {
            get
            {
                _clickCommand = _clickCommand ?? new RelayCommand(() =>
                {
                    _navigationService.NavigateTo(ViewModelLocator.DetailViewKey);
                });
                return _clickCommand;
            }
        }

        private void Initialize()
        {
            // TODO
            Temp = new List<ObservableCollection<Archive>>();
            var date = Constants.MinimumViewMonth.Date;
            while (date <= DateTime.Now)
            {
                Temp.Add(new ObservableCollection<Archive>());
                date = date.AddMonths(1);
            }
        }
    }
}