using BingoWallpaper.Configuration;
using BingoWallpaper.Models;
using BingoWallpaper.Models.LeanCloud;
using BingoWallpaper.Services;
using BingoWallpaper.Uwp.Services;
using GalaSoft.MvvmLight.Command;
using System;

namespace BingoWallpaper.Uwp.ViewModels
{
    public class DetailViewModel : NavigableViewModel
    {
        private readonly IShareService _shareService;

        private readonly ISystemSettingService _systemSettingService;

        private RelayCommand _openLockScreenSettingCommand;

        private RelayCommand _openWallpaperSettingCommand;

        private RelayCommand _saveCommand;

        private RelayCommand _setLockScreenCommand;

        private RelayCommand _setWallpaperCommand;

        private Wallpaper _wallpaper;

        private readonly IBingoWallpaperSettings _bingoWallpaperSettings;

        public DetailViewModel(ISystemSettingService systemSettingService, IShareService shareService, IBingoWallpaperSettings bingoWallpaperSettings)
        {
            _systemSettingService = systemSettingService;
            _shareService = shareService;
            _bingoWallpaperSettings = bingoWallpaperSettings;
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
                    switch (_bingoWallpaperSettings.SelectedSaveLocation)
                    {
                        case SaveLocation.PictureLibrary:
                            // TODO
                            break;

                        case SaveLocation.ChooseEveryTime:
                            // TODO
                            break;

                        case SaveLocation.SavedPictures:
                            // TODO
                            break;

                        default:
                            throw new ArgumentOutOfRangeException();
                    }

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

        public Wallpaper Wallpaper
        {
            get
            {
                return _wallpaper;
            }
            private set
            {
                Set(ref _wallpaper, value);
            }
        }

        public override void OnNavigatedTo(object parameter)
        {
            base.OnNavigatedTo(parameter);

            Wallpaper = (Wallpaper)parameter;
        }
    }
}