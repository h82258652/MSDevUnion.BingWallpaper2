using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace BingoWallpaper.ViewModels
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<DetailViewModel>();
        }

        public DetailViewModel Detail
        {
            get
            {
                return ServiceLocator.Current.GetInstance<DetailViewModel>();
            }
        }
    }
}