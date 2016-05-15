﻿using BingoWallpaper.Service;
using BingoWallpaper.Uwp.Service;
using BingoWallpaper.Uwp.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;

namespace BingoWallpaper.Uwp.ViewModel
{
    public class ViewModelLocator
    {
        public const string DetailViewKey = @"Detail";

        public const string SettingViewKey = @"Setting";

        public const string AboutViewKey = @"About";

        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<INavigationService>(CreateNavigationService);
            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<IWallpaperService, Design.WallpaperService>();
            }
            else
            {
                SimpleIoc.Default.Register<IWallpaperService, WallpaperService>();
            }
            SimpleIoc.Default.Register<ISystemSettingService, SystemSettingService>();

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<DetailViewModel>();
            SimpleIoc.Default.Register<SettingViewModel>();
            SimpleIoc.Default.Register<AboutViewModel>();
        }

        private static INavigationService CreateNavigationService()
        {
            var navigationService = new NavigationService();
            navigationService.Configure(DetailViewKey, typeof(DetailView));
            navigationService.Configure(SettingViewKey, typeof(SettingView));
            navigationService.Configure(AboutViewKey, typeof(AboutView));
            return navigationService;
        }

        public AboutViewModel About => ServiceLocator.Current.GetInstance<AboutViewModel>();

        public DetailViewModel Detail => ServiceLocator.Current.GetInstance<DetailViewModel>();

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();

        public SettingViewModel Setting => ServiceLocator.Current.GetInstance<SettingViewModel>();
    }
}