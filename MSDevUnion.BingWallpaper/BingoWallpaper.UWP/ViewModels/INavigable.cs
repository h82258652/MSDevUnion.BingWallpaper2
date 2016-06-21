namespace BingoWallpaper.Uwp.ViewModels
{
    public interface INavigable
    {
        void OnNavigatedFrom();

        void OnNavigatedTo(object parameter);

        bool OnNavigatingFrom();
    }
}