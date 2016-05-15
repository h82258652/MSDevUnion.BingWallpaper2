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

        private bool _isBusy;

        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            Initialize();
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
    }
}