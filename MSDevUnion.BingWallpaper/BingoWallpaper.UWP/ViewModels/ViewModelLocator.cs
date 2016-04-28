using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace BingoWallpaper.UWP.ViewModels
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<DetailViewModel>();
            SimpleIoc.Default.Register<SettingViewModel>();
            SimpleIoc.Default.Register<AboutViewModel>();
        }

        public AboutViewModel About
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AboutViewModel>();
            }
        }

        public DetailViewModel Detail
        {
            get
            {
                return ServiceLocator.Current.GetInstance<DetailViewModel>();
            }
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public SettingViewModel Setting
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SettingViewModel>();
            }
        }
    }
}