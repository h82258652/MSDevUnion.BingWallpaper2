using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace BingoWallpaper.UWP.ViewModels
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