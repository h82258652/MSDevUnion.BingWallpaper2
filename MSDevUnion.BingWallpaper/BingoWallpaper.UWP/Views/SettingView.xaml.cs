using BingoWallpaper.Uwp.ViewModels;
using Windows.UI.Xaml.Input;

namespace BingoWallpaper.Uwp.Views
{
    public sealed partial class SettingView
    {
        public SettingView()
        {
            InitializeComponent();
        }

        public SettingViewModel ViewModel => (SettingViewModel)DataContext;

        protected override void OnNavigationBackRequested(NavigationBackRequestedEventArgs e)
        {
            base.OnNavigationBackRequested(e);

            if (Frame.CanGoBack)
            {
                e.Handled = true;
                Frame.GoBack();
            }
        }

        protected override void OnXButton1Released(PointerRoutedEventArgs e)
        {
            base.OnXButton1Released(e);

            if (Frame.CanGoBack)
            {
                e.Handled = true;
                Frame.GoBack();
            }
        }
    }
}