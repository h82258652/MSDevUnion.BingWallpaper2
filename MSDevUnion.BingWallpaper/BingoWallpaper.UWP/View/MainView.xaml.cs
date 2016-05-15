using BingoWallpaper.Uwp.ViewModel;
using Windows.UI.Xaml.Controls;

namespace BingoWallpaper.Uwp.View
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