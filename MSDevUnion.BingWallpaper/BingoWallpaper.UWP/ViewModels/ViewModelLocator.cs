using BingoWallpaper.Services;
using BingoWallpaper.Uwp.Configuration;
using BingoWallpaper.Uwp.Services;
using BingoWallpaper.Uwp.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;

namespace BingoWallpaper.Uwp.ViewModels
{
    public class ViewModelLocator
    {
        public const string AboutViewKey = @"About";

        public const string DetailViewKey = @"Detail";

        public const string SettingViewKey = @"Setting";

        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<IBingoWallpaperSettings>(() => BingoWallpaperSettings.Current);

            SimpleIoc.Default.Register<INavigationService>(CreateNavigationService);
            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<IWallpaperService, Design.WallpaperService>();
            }
            else
            {
                SimpleIoc.Default.Register<IWallpaperService, WallpaperWithCacheService>();
            }
            SimpleIoc.Default.Register<ISystemSettingService, SystemSettingService>();

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<DetailViewModel>();
            SimpleIoc.Default.Register<SettingViewModel>();
            SimpleIoc.Default.Register<AboutViewModel>();
        }

        public AboutViewModel About => ServiceLocator.Current.GetInstance<AboutViewModel>();

        public DetailViewModel Detail => ServiceLocator.Current.GetInstance<DetailViewModel>();

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();

        public SettingViewModel Setting => ServiceLocator.Current.GetInstance<SettingViewModel>();

        private static INavigationService CreateNavigationService()
        {
            var navigationService = new NavigationService();
            navigationService.Configure(DetailViewKey, typeof(DetailView));
            navigationService.Configure(SettingViewKey, typeof(SettingView));
            navigationService.Configure(AboutViewKey, typeof(AboutView));
            return navigationService;
        }
    }
}