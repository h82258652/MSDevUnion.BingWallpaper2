using BingoWallpaper.Uwp.ViewModel;

namespace BingoWallpaper.Uwp.View
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