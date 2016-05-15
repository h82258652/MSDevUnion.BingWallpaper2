using Windows.UI.Xaml.Input;

namespace BingoWallpaper.Uwp.View
{
    public sealed partial class AboutView
    {
        public AboutView()
        {
            InitializeComponent();
        }

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