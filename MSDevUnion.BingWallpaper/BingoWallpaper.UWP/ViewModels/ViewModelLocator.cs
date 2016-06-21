using BingoWallpaper.Configuration;
using BingoWallpaper.Services;
using BingoWallpaper.Uwp.Services;
using BingoWallpaper.Uwp.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace BingoWallpaper.Uwp.ViewModels
{
    public class ViewModelLocator
    {
        public const string AboutViewKey = @"About";

        public const string DetailViewKey = @"Detail";

        public const string SettingViewKey = @"Setting";

        private static IUnityContainer ConfigureUnityContainer()
        {
            var unityContainer = new UnityContainer();

            unityContainer.RegisterInstance(CreateNavigationService());

            if (ViewModelBase.IsInDesignModeStatic)
            {
                unityContainer.RegisterType<ILeanCloudWallpaperService, Design.LeanCloudWallpaperService>();
                unityContainer.RegisterType<IWallpaperService, Design.LeanCloudWallpaperService>();
            }
            else
            {
                unityContainer.RegisterType<ILeanCloudWallpaperService, LeanCloudWallpaperWithCacheService>();
                unityContainer.RegisterType<IWallpaperService, LeanCloudWallpaperWithCacheService>();
            }
            unityContainer.RegisterType<ISystemSettingService, SystemSettingService>();
            unityContainer.RegisterType<IShareService, ShareService>();

            unityContainer.RegisterType<IBingoWallpaperSettings, BingoWallpaperSettings>();

            unityContainer.RegisterType<MainViewModel>();
            unityContainer.RegisterType<DetailViewModel>();
            unityContainer.RegisterType<SettingViewModel>();
            unityContainer.RegisterType<AboutViewModel>();

            return unityContainer;
        }

        static ViewModelLocator()
        {
            var serviceLocator = new UnityServiceLocator(ConfigureUnityContainer());
            ServiceLocator.SetLocatorProvider(() => serviceLocator);

            //ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            //SimpleIoc.Default.Register(CreateNavigationService);
            //if (ViewModelBase.IsInDesignModeStatic)
            //{
            //    SimpleIoc.Default.Register<IWallpaperService, Design.LeanCloudWallpaperService>();
            //}
            //else
            //{
            //    SimpleIoc.Default.Register<ILeanCloudWallpaperService, LeanCloudWallpaperWithCacheService>();
            //    SimpleIoc.Default.Register<IWallpaperService, LeanCloudWallpaperWithCacheService>();
            //}
            //SimpleIoc.Default.Register<ISystemSettingService, SystemSettingService>();

            //SimpleIoc.Default.Register<IBingoWallpaperSettings, BingoWallpaperSettings>();

            //SimpleIoc.Default.Register<MainViewModel>();
            //SimpleIoc.Default.Register<DetailViewModel>();
            //SimpleIoc.Default.Register<SettingViewModel>();
            //SimpleIoc.Default.Register<AboutViewModel>();

            //SimpleIoc.Default.Register<IWallpaperContext, WallpaperContext>();
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