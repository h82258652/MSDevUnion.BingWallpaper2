using BingoWallpaper.Uwp.Configuration;
using GalaSoft.MvvmLight;
using Windows.Storage;

namespace BingoWallpaper.Uwp.ViewModels
{
    public class SettingViewModel : ViewModelBase
    {
        private readonly ISettings _settings;

        public SettingViewModel(ISettings settings)
        {
            _settings = settings;
        }

        public string Area
        {
            get
            {
                return _settings.Get(nameof(Area), () => "", ApplicationDataLocality.Roaming);
            }
            set
            {
                _settings.Set(nameof(Area), value, ApplicationDataLocality.Roaming);
                RaisePropertyChanged();
            }
        }

        public string SaveLocation
        {
            get
            {
                return _settings.Get(nameof(SaveLocation), () => "");
            }
            set
            {
                _settings.Set(nameof(SaveLocation), value);
                RaisePropertyChanged();
            }
        }

        public string WallpaperSize
        {
            get
            {
                return _settings.Get(nameof(WallpaperSize), () => "");
            }
            set
            {
                _settings.Set(nameof(WallpaperSize), value);
                RaisePropertyChanged();
            }
        }
    }
}