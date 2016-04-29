using Windows.Phone.UI.Input;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace BingoWallpaper.UWP.Controls
{
    public abstract class NavigationPageBase : Page
    {
        protected virtual void NavigationBackRequested(NavigationBackRequestedEventArgs e)
        {
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            SystemNavigationManager.GetForCurrentView().BackRequested -= PageBase_BackRequested;
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            SystemNavigationManager.GetForCurrentView().BackRequested += PageBase_BackRequested;
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            }
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.BackPressedEventArgs"))
            {
                var args = new NavigationBackRequestedEventArgs()
                {
                    Handled = e.Handled
                };
                NavigationBackRequested(args);
                e.Handled = args.Handled;
            }
        }

        private void PageBase_BackRequested(object sender, BackRequestedEventArgs e)
        {
            var args = new NavigationBackRequestedEventArgs()
            {
                Handled = e.Handled
            };
            NavigationBackRequested(args);
            e.Handled = args.Handled;
        }
    }
}