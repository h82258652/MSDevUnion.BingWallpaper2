using BingoWallpaper.UWP.ViewModel;
using Windows.UI.Xaml.Controls;

namespace BingoWallpaper.UWP.View
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