using BingoWallpaper.Uwp.ViewModels;

namespace BingoWallpaper.Uwp.Views
{
    public sealed partial class DetailView
    {
        public DetailView()
        {
            InitializeComponent();
        }

        public DetailViewModel ViewModel => (DetailViewModel)DataContext;
    }
}