using BingoWallpaper.Uwp.Service;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace BingoWallpaper.Uwp.ViewModel
{
    public class DetailViewModel : ViewModelBase
    {
        private readonly ISystemSettingService _systemSettingService;

        private RelayCommand _openLockScreenSettingCommand;

        private RelayCommand _openWallpaperSettingCommand;

        private RelayCommand _saveCommand;

        private RelayCommand _setLockScreenCommand;

        private RelayCommand _setWallpaperCommand;

        public DetailViewModel(ISystemSettingService systemSettingService)
        {
            _systemSettingService = systemSettingService;
        }

        public RelayCommand OpenLockScreenSettingCommand
        {
            get
            {
                _openLockScreenSettingCommand = _openLockScreenSettingCommand ?? new RelayCommand(async () =>
                {
                    await _systemSettingService.OpenLockScreenSettingAsync();
                });
                return _openLockScreenSettingCommand;
            }
        }

        public RelayCommand OpenWallpaperSettingCommand
        {
            get
            {
                _openWallpaperSettingCommand = _openWallpaperSettingCommand ?? new RelayCommand(async () =>
                {
                    await _systemSettingService.OpenWallpaperSettingAsync();
                });
                return _openWallpaperSettingCommand;
            }
        }

        public RelayCommand SaveCommand
        {
            get
            {
                _saveCommand = _saveCommand ?? new RelayCommand(() =>
                {
                    // TODO
                });
                return _saveCommand;
            }
        }

        public RelayCommand SetLockScreenCommand
        {
            get
            {
                _setLockScreenCommand = _setLockScreenCommand ?? new RelayCommand(() =>
                {
                    // TODO
                });
                return _setLockScreenCommand;
            }
        }

        public RelayCommand SetWallpaperCommand
        {
            get
            {
                _setWallpaperCommand = _setWallpaperCommand ?? new RelayCommand(() =>
                {
                    // TODO
                });
                return _setWallpaperCommand;
            }
        }
    }
}