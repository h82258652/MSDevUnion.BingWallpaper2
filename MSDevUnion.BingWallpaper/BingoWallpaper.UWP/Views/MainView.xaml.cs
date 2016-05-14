using BingoWallpaper.UWP.ViewModels;

namespace BingoWallpaper.UWP.Views
{
    public sealed partial class MainView
    {
        public MainView()
        {
            InitializeComponent();
        }

        public MainViewModel ViewModel => (MainViewModel)DataContext;
    }
}