using BingoWallpaper.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BingoWallpaper.Uwp.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        private RelayCommand _aboutCommand;

        private bool _isBusy;

        private RelayCommand _settingCommand;

        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            Initialize();
        }

        public RelayCommand AboutCommand
        {
            get
            {
                _aboutCommand = _aboutCommand ?? new RelayCommand(() =>
                {
                    _navigationService.NavigateTo(ViewModelLocator.AboutViewKey);
                });
                return _aboutCommand;
            }
        }

        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            private set
            {
                Set(ref _isBusy, value);
            }
        }

        public IList<ObservableCollection<Model.Archive>> Temp;

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

        public RelayCommand SettingCommand
        {
            get
            {
                _settingCommand = _settingCommand ?? new RelayCommand(() =>
                {
                    _navigationService.NavigateTo(ViewModelLocator.SettingViewKey);
                });
                return _settingCommand;
            }
        }
    }
}