using Windows.Phone.UI.Input;
using Windows.UI.Core;
using Windows.UI.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace BingoWallpaper.Uwp.Views
{
    public abstract class ViewBase : Page
    {
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = Frame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
            SystemNavigationManager.GetForCurrentView().BackRequested -= SystemNavigationManager_BackRequested;
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = Frame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
            SystemNavigationManager.GetForCurrentView().BackRequested += SystemNavigationManager_BackRequested;
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            }
        }

        protected virtual void OnNavigationBackRequested(NavigationBackRequestedEventArgs e)
        {
        }

        protected override void OnPointerReleased(PointerRoutedEventArgs e)
        {
            base.OnPointerReleased(e);

            var properties = e.GetCurrentPoint(null).Properties;
            switch (properties.PointerUpdateKind)
            {
                case PointerUpdateKind.XButton1Released:
                    OnXButton1Released(e);
                    break;

                case PointerUpdateKind.XButton2Released:
                    OnXButton2Released(e);
                    break;
            }
        }

        protected virtual void OnXButton1Released(PointerRoutedEventArgs e)
        {
        }

        protected virtual void OnXButton2Released(PointerRoutedEventArgs e)
        {
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.BackPressedEventArgs"))
            {
                OnNavigationBackRequested(new NavigationBackRequestedEventArgs(e));
            }
        }

        private void SystemNavigationManager_BackRequested(object sender, BackRequestedEventArgs e)
        {
            OnNavigationBackRequested(new NavigationBackRequestedEventArgs(e));
        }
    }
}