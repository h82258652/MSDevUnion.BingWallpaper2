using BingoWallpaper.Uwp.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BingoWallpaper.Uwp.Views
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