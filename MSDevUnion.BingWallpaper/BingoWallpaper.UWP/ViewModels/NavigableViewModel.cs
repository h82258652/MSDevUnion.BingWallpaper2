using GalaSoft.MvvmLight;

namespace BingoWallpaper.Uwp.ViewModels
{
    public class NavigableViewModel : ViewModelBase, INavigable
    {
        public virtual void OnNavigatedFrom()
        {
        }

        public virtual void OnNavigatedTo(object parameter)
        {
        }

        public virtual bool OnNavigatingFrom()
        {
            return false;
        }
    }
}